import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Article } from '../Interfaces/Article';
import { Headers } from '../utils/headers.utils';

@Injectable({
  providedIn: 'root',
})
export class ArticleService {
  private Base_URL = 'https://localhost:7035/api/article';
  private Header = this.headers.getHeaders();
  constructor(private http: HttpClient, private headers: Headers) {}
  getAll() {
    return this.http.get<Article[]>(this.Base_URL, { headers: this.Header });
  }
  getById(id: number) {
    return this.http.get<Article>(this.Base_URL + `/${id}`, {
      headers: this.Header,
    });
  }
}
