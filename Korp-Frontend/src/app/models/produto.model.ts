export interface Produto {
    id: string;
    codigo: string;
    descricao: string;
    saldo: number;
}

export interface ProdutoCreate {
  codigo: string;
  descricao: string;
  saldo: number;
}