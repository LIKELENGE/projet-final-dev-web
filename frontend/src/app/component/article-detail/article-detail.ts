import { Component, OnInit, inject } from '@angular/core';
import { DatePipe } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { AnnonceService } from '../../services/annonce.service';
import { imageUrl } from '../../shared/image-url';

@Component({
  selector: 'app-article-detail',
  imports: [RouterLink, DatePipe],
  templateUrl: './article-detail.html',
  styleUrl: './article-detail.css',
})
export class ArticleDetail implements OnInit {
  private readonly route = inject(ActivatedRoute);
  readonly annonceService = inject(AnnonceService);

  ngOnInit(): void {
    const annonceId = Number(this.route.snapshot.paramMap.get('annonceId'));

    if (annonceId > 0) {
      this.annonceService.consulter(annonceId);
    }
  }

  imageSrc = imageUrl;
}
