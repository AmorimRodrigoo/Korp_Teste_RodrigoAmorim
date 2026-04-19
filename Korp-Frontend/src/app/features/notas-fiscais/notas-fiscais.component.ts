import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { NotaFiscalService } from '../../services/nota-fiscal.service';
import { ProdutoService } from '../../services/produto.service';
import { NotaFiscal, NotaFiscalItem } from '../../models/nota-fiscal.model';
import { Produto } from '../../models/produto.model';

@Component({
  selector: 'app-notas-fiscais',
  standalone: true,
  imports: [
    CommonModule, FormsModule, MatTableModule, MatButtonModule, 
    MatInputModule, MatFormFieldModule, MatSelectModule, 
    MatIconModule, MatSnackBarModule, MatProgressSpinnerModule
  ],
  templateUrl: './notas-fiscais.component.html'
})
export class NotasFiscaisComponent implements OnInit {
  private nfService = inject(NotaFiscalService);
  private produtoService = inject(ProdutoService);
  private snackBar = inject(MatSnackBar);

  notas = signal<NotaFiscal[]>([]);
  produtosDisponiveis = signal<Produto[]>([]);
  colunas: string[] = ['numero', 'data', 'status', 'itens', 'acoes'];
  
  // Controle de criação da nota
  novosItens = signal<NotaFiscalItem[]>([]);
  itemSendoAdicionado: NotaFiscalItem = { produtoCodigo: '', quantidade: 1 };
  
  // Estado de carregamento para simular o Polly
  estaProcessando = signal<boolean>(false);

  ngOnInit() {
    this.carregarNotas();
    this.carregarProdutosParaSelecao();
  }

  carregarNotas() {
    this.nfService.obterTodas().subscribe(dados => this.notas.set(dados));
  }

  carregarProdutosParaSelecao() {
    this.produtoService.obterTodos().subscribe(dados => this.produtosDisponiveis.set(dados));
  }

  adicionarItemNaLista() {
    if (!this.itemSendoAdicionado.produtoCodigo || this.itemSendoAdicionado.quantidade <= 0) return;
    
    this.novosItens.update(itens => [...itens, { ...this.itemSendoAdicionado }]);
    this.itemSendoAdicionado = { produtoCodigo: '', quantidade: 1 };
  }

  removerItemDaLista(index: number) {
    this.novosItens.update(itens => itens.filter((_, i) => i !== index));
  }

  salvarNota() {
    if (this.novosItens().length === 0) return;

    this.nfService.criar({ itens: this.novosItens() }).subscribe({
      next: () => {
        this.mostrarMensagem('Nota Fiscal aberta com sucesso!', 'sucesso'); // <-- Sucesso
        this.novosItens.set([]);
        this.carregarNotas();
      },
      error: (err) => this.mostrarMensagem('Erro ao criar nota.', 'erro') // <-- Erro
    });
  }

  imprimirNota(id: string) {
    this.estaProcessando.set(true);
    // Este aviso inicial pode ser azul/cinza padrão, ou sucesso. Vamos deixar sucesso:
    this.mostrarMensagem('Iniciando faturamento e baixa de estoque...', 'sucesso');

    this.nfService.imprimir(id).subscribe({
      next: (res) => {
        this.mostrarMensagem(res.mensagem, 'sucesso'); // <-- Sucesso (Verde)
        this.carregarNotas();
        this.estaProcessando.set(false);
      },
      error: (err) => {
        // O AVISO DO POLLY AGORA SERÁ VERMELHO, NO TOPO E BEM DESTACADO!
        this.mostrarMensagem(err.error?.mensagem || 'Serviço de estoque indisponível.', 'erro'); 
        this.estaProcessando.set(false);
      }
    });
  }

  private mostrarMensagem(msg: string, tipo: 'sucesso' | 'erro' = 'sucesso') {
    this.snackBar.open(msg, 'FECHAR', { 
      duration: tipo === 'erro' ? 7000 : 3000, // Deixa o aviso do Polly por 7 segundos na tela
      horizontalPosition: 'center', // Centralizado
      verticalPosition: 'top',      // No topo
      panelClass: tipo === 'sucesso' ? ['snackbar-sucesso'] : ['snackbar-erro'] // Aplica a cor
    });
  }
}