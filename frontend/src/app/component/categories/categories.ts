import { Component, OnInit, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CategorieService } from '../../services/categorie.service';
import { Pagination } from '../../shared/pagination/pagination';

@Component({
  selector: 'app-categories',
  imports: [RouterLink, Pagination],
  templateUrl: './categories.html',
  styleUrl: './categories.css',
})
export class Categories implements OnInit {
  readonly taillePage = 8;
  readonly categorieService = inject(CategorieService);

  ngOnInit(): void {
    this.chargerPage();
  }

  chargerPage(page = 1): void {
    this.categorieService.charger(page, this.taillePage);
  }
}
