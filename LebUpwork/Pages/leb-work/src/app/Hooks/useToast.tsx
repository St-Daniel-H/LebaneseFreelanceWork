import { toast } from "sonner";
export default function useToast({
  status,
  description,
}: {
  status: string;
  description: string;
}) {
  if (status == "success") {
    toast.success(description);
  } else if (status == "error") {
    toast.error(description);
  } else {
    toast(description);
  }
}
