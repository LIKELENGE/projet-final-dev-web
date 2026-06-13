import { Component, OnInit } from '@angular/core';
import { RouterLinkActive, RouterLink } from "@angular/router";

@Component({
  selector: 'app-navbar',

  imports: [RouterLinkActive, RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar implements OnInit {
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
}
