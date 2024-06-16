import { StringFilter } from "../../util/StringFilter";
import { StringNullableFilter } from "../../util/StringNullableFilter";
import { DateTimeNullableFilter } from "../../util/DateTimeNullableFilter";
import { AuthorWhereUniqueInput } from "../author/AuthorWhereUniqueInput";

export type BookWhereInput = {
  id?: StringFilter;
  title?: StringNullableFilter;
  description?: StringNullableFilter;
  isbn?: StringNullableFilter;
  publishedDate?: DateTimeNullableFilter;
  author?: AuthorWhereUniqueInput;
};
