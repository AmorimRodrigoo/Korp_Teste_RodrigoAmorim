import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ProdutoService } from '../../services/produto.service';
import { Produto, ProdutoCreate } from '../../models/produto.model';

@Component({
  selector: 'app-produtos',
  standalone: true,
  imports: [
    CommonModule, FormsModule, MatTableModule, MatButtonModule, 
    MatInputModule, MatFormFieldModule, MatSnackBarModule
  ],
  templateUrl: './produtos.component.html'
})
export class ProdutosComponent implements OnInit {
  private produtoService = inject(ProdutoService);
  private snackBar = inject(MatSnackBar);

  produtos = signal<Produto[]>([]);
  colunas: string[] = ['codigo', 'descricao', 'saldo'];
  
  novoProduto: ProdutoCreate = { codigo: '', descricao: '', saldo: 0 };

  ngOnInit() {
    this.carregarProdutos();
  }

  carregarProdutos() {
    this.produtoService.obterTodos().subscribe({
      next: (dados) => this.produtos.set(dados),
      error: (err) => this.mostrarMensagem('Erro ao carregar produtos')
    });
  }

  salvarProduto() {
    if (!this.novoProduto.codigo || !this.novoProduto.descricao) {
      this.mostrarMensagem('Preencha os campos obrigatórios!');
      return;
    }

    this.produtoService.criar(this.novoProduto).subscribe({
      next: (produtoCriado) => {
        this.produtos.update(lista => [...lista, produtoCriado]);
        this.mostrarMensagem('Produto salvo com sucesso!');
        this.novoProduto = { codigo: '', descricao: '', saldo: 0 }; 
      },
      error: (err) => {
        this.mostrarMensagem(err.error?.mensagem || 'Erro ao salvar produto');
      }
    });
  }

  private mostrarMensagem(msg: string, tipo: 'sucesso' | 'erro' = 'sucesso') {
    this.snackBar.open(msg, 'FECHAR', { 
      duration: tipo === 'erro' ? 6000 : 3000, 
      horizontalPosition: 'center', 
      verticalPosition: 'top',      
      panelClass: tipo === 'sucesso' ? ['snackbar-sucesso'] : ['snackbar-erro'] 
    });
  }
}