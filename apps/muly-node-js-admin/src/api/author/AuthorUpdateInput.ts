import { BookUpdateManyWithoutAuthorsInput } from "./BookUpdateManyWithoutAuthorsInput";

export type AuthorUpdateInput = {
  firstName?: string | null;
  lastName?: string | null;
  dob?: Date | null;
  biography?: string | null;
  books?: BookUpdateManyWithoutAuthorsInput;
};
