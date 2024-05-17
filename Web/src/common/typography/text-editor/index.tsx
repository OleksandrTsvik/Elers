import { CKEditor } from '@ckeditor/ckeditor5-react';
import Editor from 'ckeditor5-custom-build';
import { Key } from 'react';

import useLocale from '../../../hooks/use-locale';

interface Props {
  editorKey: Key | null | undefined;
  text: string;
  onChange: (text: string) => void;
}

export default function TextEditor({ editorKey, text, onChange }: Props) {
  const { locale } = useLocale();

  return (
    <CKEditor
      key={`${locale}-${editorKey}`}
      editor={Editor}
      config={{ language: locale }}
      data={text}
      onChange={(_, editor) => onChange(editor.getData())}
    />
  );
}
