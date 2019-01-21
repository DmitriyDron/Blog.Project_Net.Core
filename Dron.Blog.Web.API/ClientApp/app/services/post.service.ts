
import { Http } from '@angular/http';
import { Injectable, Output, Inject } from '@angular/core';

import { Post } from '../models/post.model';
import { QueryResultPost } from '../models/query-result-post.model';
import { PostQuery } from '../models/post-query.model';
import { SavePost } from '../models/save-post.model';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';




@Injectable()
export class PostService {
    private readonly vehiclesEndpoint = '/api/posts';
    constructor(private http: Http) {
    }
    getPosts(): Observable<Post[]> {
        return this.http.get(this.vehiclesEndpoint,{ withCredentials: true }).pipe(
                map((res: any) => res.json() as Post[])
            );
    }
    getPostsQuery(postQury: PostQuery): Observable<QueryResultPost> {
        return this.http.get(`${this.vehiclesEndpoint}/query?${this.toQueryString(postQury)}`).pipe(
                map((res: any) => res.json() as QueryResultPost)
            );
    }
    getAdminQuery(postQury: PostQuery): Observable<QueryResultPost> {
        return this.http.get(`${this.vehiclesEndpoint,{ withCredentials: true }}/admin?${this.toQueryString(postQury)}`).pipe(
                map((res: any) => res.json() as QueryResultPost)
            );
    }
    getPost(id: number): Observable<Post> {
        return this.http.get(`${this.vehiclesEndpoint}/${id}`).pipe(
                map((res: any) => res.json() as Post)
            );
    }
    create(savePost: SavePost): Observable<number>{
        return this.http.post(this.vehiclesEndpoint, savePost,{ withCredentials: true }).pipe(
            map((res: any) => res.json() as number)
        );
    }
    delete(id: number): Observable<number>{
        return this.http.delete(`${this.vehiclesEndpoint,{ withCredentials: true }}/${id}`).pipe(
                map((res: any) => res.json() as number)
            );
    }
    update(id: number, post: SavePost): Observable<number> {
        return this.http.put(`${this.vehiclesEndpoint}/${id}`, post).pipe(
                map((res: any) => res.json() as number)
            );
    }
    private toQueryString(obj: any) {
        let parts: string[] = [];
        for(let prop in obj) {
            let val = obj[prop];
            if (val != null && val != undefined)
                parts.push(encodeURIComponent(prop) + '=' + encodeURIComponent(val));
        }
        return parts.join('&');
    }
}
