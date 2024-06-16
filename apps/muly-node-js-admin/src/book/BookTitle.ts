import { Book as TBook } from "../api/book/Book";

export const BOOK_TITLE_FIELD = "title";

export const BookTitle = (record: TBook): string => {
  return record.title?.toString() || String(record.id);
};
