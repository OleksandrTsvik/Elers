import { SortOrder } from 'antd/es/table/interface';

export interface PagedList<T> {
  items: T[];
  currentPage: number;
  totalPages: number;
  pageSize: number;
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

export interface PagingParams {
  pageNumber?: number;
  pageSize?: number;
}

export interface SortParams {
  sortColumn?: string;
  sortOrder?: SortOrder;
}
