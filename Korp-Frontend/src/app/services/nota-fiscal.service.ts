import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NotaFiscal, NotaFiscalCreate } from '../models/nota-fiscal.model';

@Injectable({
  providedIn: 'root'
})
export class NotaFiscalService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7052/api/notasfiscais'; 

  obterTodas(): Observable<NotaFiscal[]> {
    return this.http.get<NotaFiscal[]>(this.apiUrl);
  }

  criar(nota: NotaFiscalCreate): Observable<NotaFiscal> {
    return this.http.post<NotaFiscal>(this.apiUrl, nota);
  }

  // conexao entre faturamento e estoque
  imprimir(id: string): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}/imprimir`, {});
  }
}