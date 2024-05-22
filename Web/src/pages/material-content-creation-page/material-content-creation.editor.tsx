import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useCreateCourseMaterialContentMutation } from '../../api/course-materials.api';
import { MaterialContentEditor } from '../../shared';

interface Props {
  courseId: string;
  tabId: string;
}

export default function MaterialContentCreationEditor({
  courseId,
  tabId,
}: Props) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const [createCourseMaterial, { isLoading, error }] =
    useCreateCourseMaterialContentMutation();

  const handleSubmit = async (content: string) => {
    await createCourseMaterial({ tabId, content })
      .unwrap()
      .then(() => navigate(`/courses/edit/${courseId}`));
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
