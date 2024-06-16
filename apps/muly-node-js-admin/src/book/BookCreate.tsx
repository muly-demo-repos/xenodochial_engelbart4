import * as React from "react";

import {
  Create,
  SimpleForm,
  CreateProps,
  TextInput,
  DateTimeInput,
  ReferenceInput,
  SelectInput,
} from "react-admin";

import { AuthorTitle } from "../author/AuthorTitle";

export const BookCreate = (props: CreateProps): React.ReactElement => {
  return (
    <Create {...props}>
      <SimpleForm>
        <TextInput label="title" source="title" />
        <TextInput label="description" multiline source="description" />
        <TextInput label="isbn" source="isbn" />
        <DateTimeInput label="publishedDate" source="publishedDate" />
        <ReferenceInput source="author.id" reference="Author" label="author">
          <SelectInput optionText={AuthorTitle} />
        </ReferenceInput>
      </SimpleForm>
    </Create>
  );
};
