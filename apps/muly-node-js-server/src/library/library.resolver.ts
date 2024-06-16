import * as graphql from "@nestjs/graphql";
import { LibraryResolverBase } from "./base/library.resolver.base";
import { Library } from "./base/Library";
import { LibraryService } from "./library.service";

@graphql.Resolver(() => Library)
export class LibraryResolver extends LibraryResolverBase {
  constructor(protected readonly service: LibraryService) {
    super(service);
  }
}
