import { SortOrder } from "../../util/SortOrder";

export type LibraryOrderByInput = {
  id?: SortOrder;
  createdAt?: SortOrder;
  updatedAt?: SortOrder;
  name?: SortOrder;
  location?: SortOrder;
};
