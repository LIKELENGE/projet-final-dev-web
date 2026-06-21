import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  imports: [],
  templateUrl: './pagination.html',
  styleUrl: './pagination.css',
})
export class Pagination {
  @Input() page = 1;
  @Input() totalPages = 0;
  @Input() total = 0;

  @Output() pageChange = new EventEmitter<number>();

  get hasPrevious(): boolean {
    return this.page > 1;
  }

  get hasNext(): boolean {
    return this.page < this.totalPages;
  }

  previous(): void {
    if (this.hasPrevious) {
      this.pageChange.emit(this.page - 1);
    }
  }

  next(): void {
    if (this.hasNext) {
      this.pageChange.emit(this.page + 1);
    }
  }
}
