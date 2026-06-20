import { Component, OnInit } from '@angular/core';
import { Router, RouterLinkActive, RouterLink } from "@angular/router";
import { UtilisateurService } from '../../services/utilisateur.service';

@Component({
  selector: 'app-navbar',

  imports: [RouterLinkActive, RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar implements OnInit {
  constructor(public utilisateurService: UtilisateurService, private router: Router) {}

  isDark = false;

  ngOnInit() {
    const savedTheme = localStorage.getItem('theme') || 'light';

    this.isDark = savedTheme === 'dark';
    document.documentElement.setAttribute('data-bs-theme', savedTheme);
  }

  toggleTheme() {
    this.isDark = !this.isDark;

    const theme = this.isDark ? 'dark' : 'light';

    document.documentElement.setAttribute('data-bs-theme', theme);
    localStorage.setItem('theme', theme);
  }

  deconnecter() {
    this.utilisateurService.deconnecter();
    this.router.navigateByUrl('/articles');
  }
}
