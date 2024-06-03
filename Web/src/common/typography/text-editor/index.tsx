import { CKEditor } from '@ckeditor/ckeditor5-react';
import Editor from 'ckeditor5-custom-build';
import { Key } from 'react';

import useLocale from '../../../hooks/use-locale';
import { CKEDITOR_UPLOAD_IMAGE_URL } from '../../../utils/ckeditor/ckeditor.constants';

interface Props {
  editorKey: Key | null | undefined;
  value?: string;
  onChange?: (text: string) => void;
}

export default function TextEditor({ editorKey, value, onChange }: Props) {
  const { locale } = useLocale();

  return (
    <CKEditor
      key={`${locale}-${editorKey}`}
      editor={Editor}
      config={{
        language: locale,
        simpleUpload: {
          uploadUrl: CKEDITOR_UPLOAD_IMAGE_URL,
          withCredentials: true,
          headers: { 'Accept-Language': locale },
        },
      }}
      data={value}
      onChange={(_, editor) => onChange && onChange(editor.getData())}
    />
  );
}
