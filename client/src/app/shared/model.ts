export type Pagination<T> = {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: T[];
};

export class ShopParams {
  brands: string[] = [];
  types: string[] = [];
  sort = 'name';
  pageNumber = 1;
  pageSize = 10;
  search = '';
}
