import { Book } from "../book/Book";

export type Author = {
  id: string;
  createdAt: Date;
  updatedAt: Date;
  firstName: string | null;
  lastName: string | null;
  dob: Date | null;
  biography: string | null;
  books?: Array<Book>;
};
