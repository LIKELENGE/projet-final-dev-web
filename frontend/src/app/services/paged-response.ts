export interface PagedResponse<T> {
  items: T[];
  page: number;
  taillePage: number;
  total: number;
  totalPages: number;
}

export function emptyPagedResponse<T>(taillePage: number): PagedResponse<T> {
  return {
    items: [],
    page: 1,
    taillePage,
    total: 0,
    totalPages: 0,
  };
}
