import { Library as TLibrary } from "../api/library/Library";

export const LIBRARY_TITLE_FIELD = "name";

export const LibraryTitle = (record: TLibrary): string => {
  return record.name?.toString() || String(record.id);
};
