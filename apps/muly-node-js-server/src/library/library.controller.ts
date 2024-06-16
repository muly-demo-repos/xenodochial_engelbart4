import * as common from "@nestjs/common";
import * as swagger from "@nestjs/swagger";
import { LibraryService } from "./library.service";
import { LibraryControllerBase } from "./base/library.controller.base";

@swagger.ApiTags("libraries")
@common.Controller("libraries")
export class LibraryController extends LibraryControllerBase {
  constructor(protected readonly service: LibraryService) {
    super(service);
  }
}
