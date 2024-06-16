import * as common from "@nestjs/common";
import * as swagger from "@nestjs/swagger";
import { MemberService } from "./member.service";
import { MemberControllerBase } from "./base/member.controller.base";

@swagger.ApiTags("members")
@common.Controller("members")
export class MemberController extends MemberControllerBase {
  constructor(protected readonly service: MemberService) {
    super(service);
  }
}
