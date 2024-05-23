import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useCreateCourseMaterialLinkMutation } from '../../api/course-materials.api';
import { MaterialLinkForm, MaterialLinkFormValues } from '../../shared';

interface Props {
  courseId: string;
  tabId: string;
}

export default function MaterialLinkCreationForm({ courseId, tabId }: Props) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const [createCourseMaterial, { isLoading, error }] =
    useCreateCourseMaterialLinkMutation();

  const handleSubmit = async (values: MaterialLinkFormValues) => {
    await createCourseMaterial({ tabId, ...values })
      .unwrap()
      .then(() => navigate(`/courses/edit/${courseId}`));
  };

  return (
    <MaterialLinkForm
      initialValues={{ title: '', link: '' }}
      textOnSubmitButton={t('actions.add')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
