import { LibraryWhereInput } from "./LibraryWhereInput";
import { LibraryOrderByInput } from "./LibraryOrderByInput";

export type LibraryFindManyArgs = {
  where?: LibraryWhereInput;
  orderBy?: Array<LibraryOrderByInput>;
  skip?: number;
  take?: number;
};
