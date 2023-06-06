import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Headers } from "./Header";
import { Article } from "../interfaces/Article";
import { ArticleEdit } from "../interfaces/ArticleEdit";

@Injectable({
  providedIn: "root",
})
export class ArticleService {
  private Base_URL = "https://localhost:7035/api/article";
  private Header = new Headers().getHeaders();
  constructor(private http: HttpClient) {}
  getAll() {
    return this.http.get<Article[]>(this.Base_URL, { headers: this.Header });
  }
  getById(id: number) {
    return this.http.get<Article>(this.Base_URL + `/${id}`, {
      headers: this.Header,
    });
  }
  getEditData(id: number) {
    return this.http.get<ArticleEdit>(this.Base_URL + `/GetEditData/${id}`, {
      headers: this.Header,
    });
  }
  create(article: FormData) {
    return this.http.post(this.Base_URL, article, {
      headers: this.Header,
    });
  }
  update(id: number, article: FormData) {
    return this.http.patch(this.Base_URL + `/${id}`, article, {
      headers: this.Header,
    });
  }
  delete(id: number) {
    return this.http.delete(this.Base_URL + `/${id}`, {
      headers: this.Header,
    });
  }
}
