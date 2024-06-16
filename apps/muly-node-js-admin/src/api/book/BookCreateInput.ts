import { AuthorWhereUniqueInput } from "../author/AuthorWhereUniqueInput";

export type BookCreateInput = {
  title?: string | null;
  description?: string | null;
  isbn?: string | null;
  publishedDate?: Date | null;
  author?: AuthorWhereUniqueInput | null;
};
