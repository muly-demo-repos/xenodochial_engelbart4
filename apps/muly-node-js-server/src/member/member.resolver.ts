import * as graphql from "@nestjs/graphql";
import { MemberResolverBase } from "./base/member.resolver.base";
import { Member } from "./base/Member";
import { MemberService } from "./member.service";

@graphql.Resolver(() => Member)
export class MemberResolver extends MemberResolverBase {
  constructor(protected readonly service: MemberService) {
    super(service);
  }
}
