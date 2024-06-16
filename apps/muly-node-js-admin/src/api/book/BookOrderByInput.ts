import { SortOrder } from "../../util/SortOrder";

export type BookOrderByInput = {
  id?: SortOrder;
  createdAt?: SortOrder;
  updatedAt?: SortOrder;
  title?: SortOrder;
  description?: SortOrder;
  isbn?: SortOrder;
  publishedDate?: SortOrder;
  authorId?: SortOrder;
};
