import { Component } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, MatToolbarModule, MatButtonModule],
  template: `
    <mat-toolbar color="primary" style="box-shadow: 0 2px 5px rgba(0,0,0,0.1); z-index: 2; position: relative;">
      <span>💼 Korp ERP</span>
      <span style="flex: 1 1 auto;"></span>
      <button mat-button routerLink="/produtos">Estoque</button>
      <button mat-button routerLink="/faturamento">Faturamento</button>
    </mat-toolbar>

    <main style="padding: 24px; max-width: 1200px; margin: 0 auto;">
      <router-outlet></router-outlet>
    </main>
  `
})
export class AppComponent {}