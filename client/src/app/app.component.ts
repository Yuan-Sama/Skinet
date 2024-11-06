import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { HttpClient } from '@angular/common/http';
import { Product } from './shared/product/model';
import { Pagination } from './shared/model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  baseUrl = 'https://localhost:5001/api/';
  title = 'Skinet';
  products: Product[] = [];

  private http = inject(HttpClient);

  ngOnInit(): void {
    this.http.get<Pagination<Product>>(this.baseUrl + 'products').subscribe({
      next: (response) => (this.products = [...response.data]),
      error: (error) => console.error(error),
      complete: () => console.log('complete'),
    });
  }
}
