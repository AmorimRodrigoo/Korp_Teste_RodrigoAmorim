import { Routes } from '@angular/router';

export const routes: Routes = [
    { 
    path: 'produtos', 
    loadComponent: () => import('./features/produtos/produtos.component').then(c => c.ProdutosComponent) 
  },
  { 
    path: 'faturamento', 
    loadComponent: () => import('./features/notas-fiscais/notas-fiscais.component').then(c => c.NotasFiscaisComponent) 
  },
  { path: '', redirectTo: '/produtos', pathMatch: 'full' }
];
