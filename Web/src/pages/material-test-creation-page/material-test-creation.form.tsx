import { useTranslation } from 'react-i18next';

import { useCreateCourseMaterialTestMutation } from '../../api/course-materials.mutations.api';
import { MaterialTestForm, MaterialTestFormValues } from '../../shared';

interface Props {
  tabId: string;
}

export default function MaterialTestCreationForm({ tabId }: Props) {
  const { t } = useTranslation();

  const [createCourseMaterial, { isLoading, error }] =
    useCreateCourseMaterialTestMutation();

  const handleSubmit = async (values: MaterialTestFormValues) => {
    await createCourseMaterial({ tabId, ...values }).unwrap();
  };

  return (
    <MaterialTestForm
      initialValues={{ title: '', numberAttempts: 1 }}
      textOnSubmitButton={t('actions.add')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
