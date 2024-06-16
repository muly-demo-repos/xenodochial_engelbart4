import { StringFilter } from "../../util/StringFilter";
import { StringNullableFilter } from "../../util/StringNullableFilter";
import { DateTimeNullableFilter } from "../../util/DateTimeNullableFilter";
import { BookListRelationFilter } from "../book/BookListRelationFilter";

export type AuthorWhereInput = {
  id?: StringFilter;
  firstName?: StringNullableFilter;
  lastName?: StringNullableFilter;
  dob?: DateTimeNullableFilter;
  biography?: StringNullableFilter;
  books?: BookListRelationFilter;
};
