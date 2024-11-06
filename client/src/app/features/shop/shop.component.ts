import { Component, inject, OnInit } from '@angular/core';
import { Product } from '../../shared/product/model';
import { ShopService } from '../../core/services/shop.service';
import { ProductItemComponent } from './product-item/product-item.component';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [ProductItemComponent],
  templateUrl: './shop.component.html',
})
export class ShopComponent implements OnInit {
  products: Product[] = [];

  private shopService = inject(ShopService);

  ngOnInit(): void {
    this.shopService.getProducts().subscribe({
      next: (response) => (this.products = [...response.data]),
      error: (error) => console.error(error),
    });
  }
}
