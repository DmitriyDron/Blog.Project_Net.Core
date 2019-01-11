import { Post } from './post.model';

export interface QueryResultPost {
	totalItems: number;
	items: Post[];
}
