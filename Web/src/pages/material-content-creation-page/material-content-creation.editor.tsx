import { useTranslation } from 'react-i18next';

import { useCreateCourseMaterialMutation } from '../../api/course-materials.api';
import { MaterialContentEditor } from '../../shared';

interface Props {
  tabId: string;
}

export default function MaterialContentCreationEditor({ tabId }: Props) {
  const { t } = useTranslation();

  const [createCourseMaterial, { isLoading, error }] =
    useCreateCourseMaterialMutation();

  const handleSubmit = async (text: string) => {
    await createCourseMaterial({ tabId, text }).unwrap();
  };

  return (
    <MaterialContentEditor
      textOnSubmitButton={t('actions.add')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
