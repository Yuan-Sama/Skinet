import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { HttpClient } from '@angular/common/http';
import { Product } from './shared/product/model';
import { Pagination } from './shared/model';
import { ShopService } from './core/services/shop.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  title = 'Skinet';
  products: Product[] = [];

  private shopService = inject(ShopService);

  ngOnInit(): void {
    this.shopService.getProducts().subscribe({
      next: (response) => (this.products = [...response.data]),
      error: (error) => console.error(error),
    });
  }
}
