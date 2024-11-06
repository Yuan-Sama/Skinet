import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Pagination } from '../../shared/model';
import { Product } from '../../shared/product/model';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  private http = inject(HttpClient);

  getProducts() {
    return this.http.get<Pagination<Product>>(
      this.baseUrl + 'products?pageSize=20'
    );
  }
}
