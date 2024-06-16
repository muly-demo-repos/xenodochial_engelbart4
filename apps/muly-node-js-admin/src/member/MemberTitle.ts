import { Member as TMember } from "../api/member/Member";

export const MEMBER_TITLE_FIELD = "firstName";

export const MemberTitle = (record: TMember): string => {
  return record.firstName?.toString() || String(record.id);
};
