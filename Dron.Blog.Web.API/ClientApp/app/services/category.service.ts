import { map } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Category } from '../models/category.model';
import { Observable } from 'rxjs/Observable';
import { SaveCategory } from '../models/save-category.model';




@Injectable()
export class CategoryService {
    private readonly categoriesEndpoint = '/api/categories';
    constructor(private http: Http) { 
    }
    getCategories(): Observable<Category[]> {
        return this.http.get(this.categoriesEndpoint,{ withCredentials: true }).pipe(
            map((res: any) => res.json() as Category[])
        );
    }
    create(savePost: SaveCategory): Observable<number>{
        return this.http.post(this.categoriesEndpoint, savePost,{ withCredentials: true }).pipe(
            map((res: any) => res.json() as number)
        );
    }
    delete(id: number): Observable<number>{
        return this.http.delete(`${this.categoriesEndpoint}/${id},`,{ withCredentials: true }).pipe(
            map((res: any) => res.json() as number)
        );
    }
}