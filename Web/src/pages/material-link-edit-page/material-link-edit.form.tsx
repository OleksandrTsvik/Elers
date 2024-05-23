import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useUpdateCourseMaterialLinkMutation } from '../../api/course-materials.api';
import { MaterialLinkForm, MaterialLinkFormValues } from '../../shared';

interface Props {
  courseId: string;
  courseMaterialId: string;
  title: string;
  link: string;
}

export default function MaterialLinkEditForm({
  courseId,
  courseMaterialId,
  title,
  link,
}: Props) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const [updateCourseMaterial, { isLoading, error }] =
    useUpdateCourseMaterialLinkMutation();

  const handleSubmit = async (values: MaterialLinkFormValues) => {
    await updateCourseMaterial({ id: courseMaterialId, ...values })
      .unwrap()
      .then(() => navigate(`/courses/edit/${courseId}`));
  };

  return (
    <MaterialLinkForm
      initialValues={{ title, link }}
      textOnSubmitButton={t('actions.save_changes')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
