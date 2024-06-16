import { Module } from "@nestjs/common";
import { LibraryModuleBase } from "./base/library.module.base";
import { LibraryService } from "./library.service";
import { LibraryController } from "./library.controller";
import { LibraryResolver } from "./library.resolver";

@Module({
  imports: [LibraryModuleBase],
  controllers: [LibraryController],
  providers: [LibraryService, LibraryResolver],
  exports: [LibraryService],
})
export class LibraryModule {}
