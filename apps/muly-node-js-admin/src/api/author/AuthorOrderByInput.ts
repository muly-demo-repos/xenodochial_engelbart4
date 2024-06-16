import { SortOrder } from "../../util/SortOrder";

export type AuthorOrderByInput = {
  id?: SortOrder;
  createdAt?: SortOrder;
  updatedAt?: SortOrder;
  firstName?: SortOrder;
  lastName?: SortOrder;
  dob?: SortOrder;
  biography?: SortOrder;
};
