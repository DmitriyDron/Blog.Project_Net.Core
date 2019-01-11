import { Category } from './category.model';
import { Tag } from './tag.model';

export interface Post {
  id: number;
  title: string;
  content: string;
  shortContent: string;
  category: Category;
  dateCreated: string;
  tags: Tag[];
}
