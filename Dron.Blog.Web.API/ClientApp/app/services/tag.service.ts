import { Tag } from '../models/tag.model';
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Http } from '@angular/http';

@Injectable()
export class TagService {
    private readonly tagsEndpoint = '/api/tags';
    constructor(private http: Http) { 
    }
    getTags(name: string, records: number): Observable<Tag[]> {
        return this.http.get(`${this.tagsEndpoint}?name=${name}&records=${records}`).pipe(
            map((res: any) => res.json() as Tag[])
        );
    }
}
