export interface SavePost {
    title: string;
    content: string;
    shortContent: string;
    categoryId: number | null;
    tags: string[];
}
