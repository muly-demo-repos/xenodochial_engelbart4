import { Injectable } from "@nestjs/common";
import { PrismaService } from "../prisma/prisma.service";
import { LibraryServiceBase } from "./base/library.service.base";

@Injectable()
export class LibraryService extends LibraryServiceBase {
  constructor(protected readonly prisma: PrismaService) {
    super(prisma);
  }
}
