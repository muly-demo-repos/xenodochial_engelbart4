import { Author } from "../author/Author";

export type Book = {
  id: string;
  createdAt: Date;
  updatedAt: Date;
  title: string | null;
  description: string | null;
  isbn: string | null;
  publishedDate: Date | null;
  author?: Author | null;
};
