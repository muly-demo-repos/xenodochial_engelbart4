import { BookCreateNestedManyWithoutAuthorsInput } from "./BookCreateNestedManyWithoutAuthorsInput";

export type AuthorCreateInput = {
  firstName?: string | null;
  lastName?: string | null;
  dob?: Date | null;
  biography?: string | null;
  books?: BookCreateNestedManyWithoutAuthorsInput;
};
