export interface NotaFiscalItem {
  produtoCodigo: string;
  quantidade: number;
}

export interface NotaFiscal {
  id: string;
  numeroSequencial: number;
  status: string; // 'Aberta' ou 'Fechada'
  dataCriacao: string;
  itens: NotaFiscalItem[];
}

export interface NotaFiscalCreate {
  itens: NotaFiscalItem[];
}