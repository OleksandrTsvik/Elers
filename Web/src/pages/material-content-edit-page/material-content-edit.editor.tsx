import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useUpdateCourseMaterialContentMutation } from '../../api/course-materials.api';
import { MaterialContentEditor } from '../../shared';

interface Props {
  courseId: string;
  courseMaterialId: string;
  content: string;
}

export default function MaterialContentEditEditor({
  courseId,
  courseMaterialId,
  content,
}: Props) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const [updateCourseMaterial, { isLoading, error }] =
    useUpdateCourseMaterialContentMutation();

  const handleSubmit = async (content: string) => {
    await updateCourseMaterial({ id: courseMaterialId, content })
      .unwrap()
      .then(() => navigate(`/courses/edit/${courseId}`));
  };

  return (
    <MaterialContentEditor
      initialText={content}
      textOnSubmitButton={t('actions.save_changes')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
