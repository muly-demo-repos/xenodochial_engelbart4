import * as React from "react";
import { Edit, SimpleForm, EditProps, TextInput } from "react-admin";

export const LibraryEdit = (props: EditProps): React.ReactElement => {
  return (
    <Edit {...props}>
      <SimpleForm>
        <TextInput label="name" source="name" />
        <TextInput label="location" source="location" />
      </SimpleForm>
    </Edit>
  );
};
