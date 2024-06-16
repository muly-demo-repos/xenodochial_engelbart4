import { MemberWhereInput } from "./MemberWhereInput";
import { MemberOrderByInput } from "./MemberOrderByInput";

export type MemberFindManyArgs = {
  where?: MemberWhereInput;
  orderBy?: Array<MemberOrderByInput>;
  skip?: number;
  take?: number;
};
